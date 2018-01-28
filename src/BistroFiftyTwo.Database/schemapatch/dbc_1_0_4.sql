create or replace function dbc_1_0_0() returns void as
$$
declare
	_old_major integer := 1;
	_old_minor integer := 0;
	_old_revision integer := 3;

	_major integer := 1;
	_minor integer := 0;
	_revision integer := 4;
	_schemaname varchar := 'recipemanager';

	_patch_exists integer := 0;
	_patch_required integer := 0;
begin
	select count(*) into _patch_exists
	from schemaversion
	where major = _major and minor = _minor and revision = _revision and schemaname = _schemaname;

	select count(*) into _patch_required
	from schemaversion
	where major = _old_major and minor = _old_minor and revision = _old_revision and schemaname = _schemaname and current_version = true;

	if(_patch_exists > 0) then
		return;
	end if;

	if (_patch_required > 0) then


		create table organizations (
			id uuid not null default(uuid_generate_v4()),
			name text not null, 
			type int not null,
			owner varchar(64) not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_organizaton_id primary key (id)
		);
		

		create table organization_accounts (
			id uuid not null default(uuid_generate_v4()),
			userlogin varchar(64) not null,
			accountpassword varchar(128) not null,
			fullname varchar(64) not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_account_id primary key (id)
		);

		create table organization_members (
			accountid uuid not null,
			organizationid uuid not null,
			accesslevel int not null,
			constraint pk_member_userorg primary key (accountid, organizationid),
			constraint fk_member_accounts foreign key (accountid) references organization_accounts (id),
			constraint fk_member_organizations foreign key (organizationid) references organizations (id)
		);


		update schemaversion set current_version = false where major = _old_major and minor = _old_minor and revision = _old_revision and schemaname = _schemaname;

		insert into schemaversion
		(major,minor,revision,schemaname,installed_date,current_version)
		values
		(_major,_minor,_revision,_schemaname,current_timestamp, true);
	else
		raise exception 'Missing prerequisite schema update %.%.% for %', _major,_minor,_revision,_schemaname;
	end if;
exception
	when others then
	raise exception 'caught exception - (%) - when applying update %.%.% to % ', SQLERRM, _major,_minor,_revision,_schemaname;

end
$$
language 'plpgsql';


select dbc_1_0_0();
drop function dbc_1_0_0();