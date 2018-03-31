BEGIN;
    drop table recipe_ingredients;
    drop table recipe_steps;
    drop table recipes;

    drop extension "uuid-ossp";
COMMIT;