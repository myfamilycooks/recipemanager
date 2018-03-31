BEGIN;

		create table organizations (
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
			constraint pk_organizaton_id primary key (id)
		);
		
        create unique index unq_organizations_urlkey on organizations (urlkey);

		create table organization_accounts (
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

		create table organization_members (
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
			constraint fk_member_organizations foreign key (organizationid) references organizations (id)
		);

        create unique index unq_members_accountorg on organization_members (accountid, organizationid);

COMMIT;