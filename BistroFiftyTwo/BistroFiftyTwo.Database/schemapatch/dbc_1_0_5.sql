create or replace function dbc_1_0_0() returns void as
$$
declare
	_old_major integer := 1;
	_old_minor integer := 0;
	_old_revision integer := 4;

	_major integer := 1;
	_minor integer := 0;
	_revision integer := 5;
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

		create table clients (
			id uuid not null default(uuid_generate_v4()),
			enabled boolean not null default(TRUE),
			protocoltype varchar(32) not null,
			requireclientsecret boolean not null default(TRUE),
			clientname varchar(64) not null,
			clienturi varchar(128) not null,
			logouri varchar(128) not null,
			requireconsent boolean not null default(TRUE),
			allowrememberconsent boolean not null default(TRUE),
			requirepkce boolean not null default(FALSE),
			allowplaintextpkce boolean not null default(FALSE),
			allowaccesstokensviabrowser boolean not null default(FALSE),
			logouturi varchar(128) not null,
			logoutsessionrequired boolean not null default(FALSE),
			allowofflineaccess boolean not null default(FALSE),
			alwaysincludeuserclaimsinidtoken boolean not null default(FALSE),
			identitytokenlifetime int not null default(300),
			accesstokenlifetime int not null default(3600),
			authorizationcodelifetime int not null default(300),
			absoluterefreshtokenlifetime int not null default(2592000),
			slidingrefreshtokenlifetime int not null default(1296000),
			refreshtokenusage varchar(64) not null default('OneTimeOnly'),
			updateaccesstokenclaimsonrefresh boolean not null default(FALSE),
			refreshtokenexpiration varchar(64) not null default('Absolute'),
			accesstokentype varchar(64) not null default('jwt'),
			enablelocallogin boolean not null default(TRUE),
			includejwtid boolean not null default(FALSE),
			alwayssetndclientclaims boolean not null default(FALSE),
			prefixclientclaims boolean not null default(TRUE),
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_clients_id primary key (id)
		);
		
		create table client_corsorigins (
			id uuid not null default(uuid_generate_v4()),
			clientid uuid not null,
			origin varchar(128) not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_clientscorsorigins_id primary key (id),
			constraint fk_clientscorsorigins_clientid foreign key (clientid) references clients (id)
		);

		create table client_claims (
			id uuid not null default(uuid_generate_v4()),
			clientid uuid not null,
			claimtype varchar(32) not null,
			claimvalue varchar(32) not null,
			valuetype varchar(32) not null,
			properties text not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_clientsclaims_id primary key (id),
			constraint fk_clientsclaims_clientid foreign key (clientid) references clients (id)
		);

		create table client_identityproviderrestrictions (
			id uuid not null default(uuid_generate_v4()),
			clientid uuid not null,
			restrictions text not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_identityproviderrestrictions_id primary key (id),
			constraint fk_identityproviderrestrictions_clientid foreign key (clientid) references clients (id)
		);
		
		create table client_allowedscopes (
			id uuid not null default(uuid_generate_v4()),
			clientid uuid not null,
			scope varchar(128) not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_allowedscopes_id primary key (id),
			constraint fk_allowedscopes_clientid foreign key (clientid) references clients (id)
		);
		
		create table client_redirecturls (
			id uuid not null default(uuid_generate_v4()),
			clientid uuid not null,
			redirecturl varchar(128) not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_redirecturls_id primary key (id),
			constraint fk_redirecturls_clientid foreign key (clientid) references clients (id)
		);
	
		create table client_postlogoutredirecturls (
			id uuid not null default(uuid_generate_v4()),
			clientid uuid not null,
			postlogoutredirecturl varchar(128) not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_postlogoutredirecturls_id primary key (id),
			constraint fk_postlogoutredirecturls_clientid foreign key (clientid) references clients (id)
		);
		
		create table client_allowedgranttypes (
			id uuid not null default(uuid_generate_v4()),
			clientid uuid not null,
			allowedgranttype varchar(128) not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_allowedgranttypes_id primary key (id),
			constraint fk_allowedgranttypes_clientid foreign key (clientid) references clients (id)
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