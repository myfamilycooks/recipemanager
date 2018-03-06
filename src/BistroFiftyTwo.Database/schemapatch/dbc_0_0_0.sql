create extension "uuid-ossp";

create or replace function dbc_0_0_0() returns void as
$$
BEGIN
	if not exists(select * from information_schema.tables
		      where table_catalog = CURRENT_CATALOG AND
			table_schema = CURRENT_SCHEMA AND
			table_name = 'schemaversion') then

		create table schemaversion
		(
		   major int not null,
		   minor int not null,
		   revision int not null,
		   schemaname varchar(64) not null,
		   installed_date timestamptz not null,
		   current_version boolean not null,
		   constraint pk_schemaversion_majorminorrevisionschema primary key (major,minor,revision,schemaname)
		);

		insert into schemaversion
		(major,minor,revision,schemaname, installed_date, current_version)
		values
		(0,0,0,'recipemanager',current_timestamp, true);
	end if;


EXCEPTION
	WHEN OTHERS THEN
	RAISE EXCEPTION 'caught exception - (%)', SQLERRM;
END;
$$
language 'plpgsql';

select dbc_0_0_0();
drop function dbc_0_0_0();