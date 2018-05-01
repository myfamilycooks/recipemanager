--0begin;

    	create table role_definitions (
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

		create table account_roles (
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

--commit;