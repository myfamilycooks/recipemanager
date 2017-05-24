create or replace function dbc_1_0_0() returns void as
$$
declare
	_old_major integer := 1;
	_old_minor integer := 0;
	_old_revision integer := 2;

	_major integer := 1;
	_minor integer := 0;
	_revision integer := 3;
	_schemaname varchar := 'identity';

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

		create table role_definitions (
			id uuid not null default(uuid_generate_v4()),
			name varchar(32) not null,
			fullname varchar(64) not null,
			description text not null,
			constraint pk_roles_id primary key (id)
		);

		create table user_roles (
			userid uuid not null,
			roleid uuid not null,
			createddate timestamptz not null default(now()),
			isdisabled boolean not null default(FALSE),
			effectiveenddate timestamptz null,
			constraint pk_userroles_useridroleid primary key (userid, roleid),
			constraint fk_userroles_users foreign key (userid) references user_accounts (id),
			constraint fk_userroles_roles foreign key (roleid) references role_definitions (id) 
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