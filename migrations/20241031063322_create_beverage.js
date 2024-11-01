// migrations/xxxx_create_beverage.js
exports.up = function(knex) {
    return knex.schema.createTable('BEVERAGE', (table) => {
      table.integer('ID').primary();
      table.string('BEVERAGE_NAME', 40);
      table.integer('CATEGORY_ID').references('ID').inTable('TYPE_BEVERAGE');
      table.string('SIZE', 1);
      table.integer('PRICE');
      table.integer('RECIPE_ID');
    });
  };
  
  exports.down = function(knex) {
    return knex.schema.dropTableIfExists('BEVERAGE');
  };
  