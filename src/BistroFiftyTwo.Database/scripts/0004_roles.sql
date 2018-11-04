--0begin;

create table if not exists role_definitions
(
	id uuid not null default(uuid_generate_v4()),
	name varchar(64) not null,
	description text not null,
	fullname text,
	createddate timestamptz not null default(now()),
	createdby varchar(64) not null,
	modifieddate timestamptz default(now()),
	modifiedby varchar(64) not null,
	constraint pk_role_id primary key (id)
);

create table if not exists account_roles
(
	accountid uuid not null,
	roleid uuid not null,
	createddate timestamptz not null default(now()),
	createdby varchar(64) not null,
	modifieddate timestamptz default(now()),
	modifiedby varchar(64) not null,
	constraint pk_accountrole_accountidroleid primary key (accountid, roleid),
	constraint fk_accountrole_accounts foreign key (accountid) references organization_accounts (id),
	constraint fk_accountrole_roles foreign key (roleid) references role_definitions (id)
);

DO $$
BEGIN
	if not exists(select 1 from role_definitions) then 

		insert into role_definitions
			(name, description, fullname, createdby, modifiedby)
		values
			('authenticated', 'Authenticated users that have signed in', 'Authenticated Users', 'root', 'root');
		insert into role_definitions
			(name, description, fullname, createdby, modifiedby)
		values
			('sysadmin', 'System Administrators Full System Access', 'System Administrators', 'root', 'root');
		insert into role_definitions
			(name, description, fullname, createdby, modifiedby)
		values
			('developer', 'Developer Users have access to in development features', 'Developers', 'root', 'root');

		-- cross join, give chef access to all.
		insert into account_roles
			(accountid, roleid, createdby, modifiedby)
		select a.id as accountid, r.id as roleid, 'root', 'root'
		from organization_accounts a, role_definitions r;

	end if;
END $$;
--commit;