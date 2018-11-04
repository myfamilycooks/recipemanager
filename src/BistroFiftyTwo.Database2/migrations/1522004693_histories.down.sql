--BEGIN;
        alter table recipe_steps drop column instructions;
        alter table recipe_steps add step text not null;

        drop table recipe_histories;
--COMMIT;