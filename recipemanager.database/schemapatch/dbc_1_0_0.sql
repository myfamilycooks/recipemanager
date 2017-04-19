create or replace function dbc_1_0_0() returns void as
$$
declare
	_old_major integer := 0;
	_old_minor integer := 0;
	_old_revision integer := 0;

	_major integer := 1;
	_minor integer := 0;
	_revision integer := 0;
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

		create table recipes (
			id uuid not null default(uuid_generate_v4()),
			title text not null,
			key varchar(64),
			tags text not null,
			description text not null,
			notes text not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_recipes_id primary key (id),
		);

		create table recipe_steps (
			id uuid not null default(uuid_generate_v4()),
			ordinal int not null,
			recipeid uuid not null,
			step text not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_steps_id primary key (id),
			constraint fk_steps_recipes foreign key recipeid references recipes (id)
		);

		create table recipe_ingredients (
			id uuid not null default(uuid_generate_v4()),
			ordinal int not null,
			recipeid uuid not null,
			quantity decimal(6,2) not null,
			units varchar(16) not null,
			ingredient varchar(64) not null,
			notes text not null,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_recioe_ingredients_id primary key (id),
			constraint fk_recioe_ingredients_recipes foreign key recipeid references recipes (id)
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
