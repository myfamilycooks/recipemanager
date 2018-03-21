create or replace function dbc_1_0_0() returns void as
$$
declare
	_old_major integer := 1;
	_old_minor integer := 0;
	_old_revision integer := 9;

	_major integer := 1;
	_minor integer := 0;
	_revision integer := 10;
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

        ----------------------------------------------------------------------------------------
        -- All Changes beneath this line 
        ----------------------------------------------------------------------------------------

		alter table recipe_histories add document tsvector;
		update recipe_histories set document = to_tsvector(fulltext) where document is null;
		create index idx_fts_recipe_search on recipe_histories using gin(document);
		
        ----------------------------------------------------------------------------------------
        -- All Changes above this line 
        ----------------------------------------------------------------------------------------

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