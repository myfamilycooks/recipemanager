--BEGIN;
 
        create table if not exists  recipe_histories (
			id uuid not null default(uuid_generate_v4()),
			recipeid uuid null,
			version int not null,
			fulltext text not null,
            document tsvector,
			createddate timestamptz not null default(now()),
			createdby varchar(64) not null,
			modifieddate timestamptz default(now()),
			modifiedby varchar(64) not null,
			constraint pk_histories_id primary key (id),
			constraint fk_histories_recipes FOREIGN key (recipeid) references recipes (id)
		);

		create index if not exists idx_fts_recipe_search on recipe_histories using gin(document);
		
		
--COMMIT;