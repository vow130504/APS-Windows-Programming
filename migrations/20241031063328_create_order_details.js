// migrations/xxxx_create_order_details.js
exports.up = function(knex) {
    return knex.schema.createTable('ORDER_DETAILS', (table) => {
      table.integer('ORDER_DETAILS_ID').primary();
      table.integer('ORDER_ID').references('ORDER_ID').inTable('ORDERS');
      table.integer('BEVERAGE_ID').references('ID').inTable('BEVERAGE');
      table.integer('QUANTITY');
      table.integer('PRICE');
      table.integer('SUBTOTAL');
    });
  };
  
  exports.down = function(knex) {
    return knex.schema.dropTableIfExists('ORDER_DETAILS');
  };
  