create or replace function dbc_1_0_0() returns void as
$$
declare
	_old_major integer := 0;
	_old_minor integer := 0;
	_old_revision integer := 0;

	_major integer := 1;
	_minor integer := 0;
	_revision integer := 0;
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

		create table user_accounts (
			id uuid not null default(uuid_generate_v4()),
			login varchar(64) not null,
			email varchar(64) not null,
			password varchar(256) not null,
			islocked boolean not null default(false),
			isdisabled boolean not null default(false),
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_user_accounts_id primary key (id)
		);

		create table organizations (
			id uuid not null default(uuid_generate_v4()),
			name varchar(64) not null,
			accesskey varchar(64) not null,
			description text not null,
			owner uuid not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_organizations_id primary key (id),
			constraint fk_organizations_user foreign key (owner) references user_accounts (id)
		);

		create table event_log (
			id uuid not null default(uuid_generate_v4()),
			eventdate timestamptz not null default(now()),
			category int not null,
			target uuid null,
			eventdata int not null,
			constraint pk_eventlog_id primary key (id)
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