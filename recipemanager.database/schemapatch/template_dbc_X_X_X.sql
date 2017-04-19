create or replace function dbc_1_0_0() returns void as
$$
declare
	_old_major integer := 0;
	_old_minor integer := 0;
	_old_revision integer := 0;

	_major integer := 1;
	_minor integer := 0;
	_revision integer := 0;
	_schemaname varchar := 'cookbookdb';
begin
	if exists(select 1 from schemaversion where major = _old_major and minor = _old_minor and revision = _old_revision and schemaname = _schemaname and current_version = true) then
					
		select 1/0; -- div / 0 ensure this will never run.  Since this is a template replace this line with your code.

		update schemaversion set current_version = false where major = _old_major and minor = _old_minor and revision = _old_revision and schemaname = _schemaname;

		insert into schemaversion
		(major,minor,revision,schemaname,installed_date,current_version)
		values
		(_major,_minor,_revision,_schemaname,current_timestamp, true);
	else
		select 'Missing prerequisite schema update ' || _schemaname || 'version ' || _major || '.' || _minor || '.' || _revision;
	end if;	
exception 
	when others then
	raise exception 'caught exception - (%) - when applying update %.%.% to % ', SQLERRM, _major,_minor,_revision,_schemaname;

end
$$
language 'plpgsql';


select dbc_1_0_0();
drop function dbc_1_0_0();
