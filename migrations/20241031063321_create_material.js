// migrations/xxxx_create_material.js
exports.up = function(knex) {
    return knex.schema.createTable('MATERIAL', (table) => {
      table.integer('ID').primary();
      table.string('MATERIAL_NAME', 40);
      table.integer('STOCK');
      table.datetime('EXP_DATE');
      table.datetime('IMPORT_DATE');
    });
  };
  
  exports.down = function(knex) {
    return knex.schema.dropTableIfExists('MATERIAL');
  };
  