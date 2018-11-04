--BEGIN;

create table  if not exists organizations
(
	id uuid not null default(uuid_generate_v4()),
	name text not null,
	description text not null,
	urlkey varchar(64) not null,
	orgtype int not null,
	owner varchar(64) not null,
	createddate timestamptz not null default(now()),
	createdby varchar(64) not null,
	modifieddate timestamptz default(now()),
	modifiedby varchar(64) not null,
	constraint pk_organizaton_id primary key (id),
	constraint unq_organizations_urlkey unique (urlkey)
);

DO $$
BEGIN
	if not exists(select 1 from organizations where name = 'System') then 
		insert into organizations
			(name, description, urlkey, orgtype, owner, createdby, modifiedby)
		values
			('System', 'My Family Cooks System organization', 'system', 0, 'chef', 'root', 'root');
	end if;
END $$;

create table if not exists  organization_accounts
(
	id uuid not null default(uuid_generate_v4()),
	userlogin varchar(64) not null,
	fullname varchar(64) not null,
	email varchar(128) not null,
	accountpassword varchar(128) not null,
	passwordformat int not null default(1),
	salt varchar(64) not null,
	islocked boolean not null default('FALSE'),
	isdisabled boolean not null default('FALSE'),
	createddate timestamptz not null default(now()),
	createdby varchar(64) not null,
	modifieddate timestamptz default(now()),
	modifiedby varchar(64) not null,
	constraint pk_account_id primary key (id),
	constraint unq_userlogin UNIQUE (userlogin)
);

DO $$
BEGIN
	if not exists(select 1 from organization_accounts where userlogin = 'chef') then 
		insert into organization_accounts
			(userlogin, fullname, email, accountpassword, passwordformat, salt, createdby, modifiedby)
		values
			('chef', 'Chef Hetfield', 'chef@recipebox.app', 'IbzlTq/c34j6bo8J5KaL3dZaFpLykGiFd+zjQFkBxYA=', 2, 'eK2DBX8KenxhIaJrE/KjlA==', 'root', 'root');
	end if;
END $$;

create table if not exists organization_members
(
	id uuid not null default(uuid_generate_v4()),
	membershipstatus int not null,
	accountid uuid not null,
	organizationid uuid not null,
	accesslevel int not null,
	createddate timestamptz not null default(now()),
	createdby varchar(64) not null,
	modifieddate timestamptz default(now()),
	modifiedby varchar(64) not null,
	constraint pk_member_id primary key (id),
	constraint fk_member_accounts foreign key (accountid) references organization_accounts (id),
	constraint fk_member_organizations foreign key (organizationid) references organizations (id),
	constraint unq_members_accountorg unique (accountid, organizationid)
);

DO $$
BEGIN
	if not exists(select 1 from organization_members) then 
		insert into organization_members
			(membershipstatus, accountid, organizationid, accesslevel, createdby, modifiedby)
		select 1 as membershipstatus, a.id as accountid, o.id as organizationid, 0 as accesslevel, 'root' as createdby, 'root' as modifiedby
		from organizations o, organization_accounts a;
	end if;
END $$;
--COMMIT;