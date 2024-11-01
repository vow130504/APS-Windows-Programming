// migrations/xxxx_create_recipe.js
exports.up = function(knex) {
    return knex.schema.createTable('RECIPE', (table) => {
      table.integer('BEVERAGE_ID').references('ID').inTable('BEVERAGE');
      table.integer('MATERIAL_ID').references('ID').inTable('MATERIAL');
      table.integer('QUANTITY');
      table.primary(['BEVERAGE_ID', 'MATERIAL_ID']);
    });
  };
  
  exports.down = function(knex) {
    return knex.schema.dropTableIfExists('RECIPE');
  };
  