--BEGIN;
    CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
    create table if not exists recipes (
        id uuid not null default(uuid_generate_v4()),
        title text not null,
        key varchar(64),
        tags text not null,
		shortdescription text null,
        description text not null,
        notes text not null,
		featured boolean default false,
        createddate timestamptz not null default(now()),
        createdby varchar(64) not null,
        modifieddate timestamptz default(now()),
        modifiedby varchar(64) not null,
        constraint pk_recipes_id primary key (id)
    );

    create table if not exists  recipe_steps (
        id uuid not null default(uuid_generate_v4()),
        ordinal int not null,
        recipeid uuid not null,
        instructions text not null,
		createddate timestamptz not null default(now()),
        createdby varchar(64) not null,
        modifieddate timestamptz default(now()),
        modifiedby varchar(64) not null,
        constraint pk_steps_id primary key (id),
        constraint fk_steps_recipes foreign key (recipeid) references recipes (id)
    );

    create table if not exists  recipe_ingredients (
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
        constraint fk_recioe_ingredients_recipes foreign key (recipeid) references recipes (id)
    );
--COMMIT;