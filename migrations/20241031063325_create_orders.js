// migrations/xxxx_create_orders.js
exports.up = function(knex) {
    return knex.schema.createTable('ORDERS', (table) => {
      table.integer('ORDER_ID').primary();
      table.integer('CUSTOMER_ID').references('CUSTOMER_ID').inTable('CUSTOMERS');
      table.integer('RESERVED_TABLE_ID').references('RESERVED_TABLE_ID').inTable('RESERVED_TABLE');
      table.datetime('ORDER_TIME');
      table.datetime('COMPLETED_TIME');
      table.integer('TOTAL_AMOUNT');
      table.integer('PAYMENT_METHOD_ID');
      table.integer('EMPLOYEE_ID').references('EMPLOYEE_ID').inTable('ACCOUNT');
      table.string('PAYMENT_METHOD', 40);
    });
  };
  
  exports.down = function(knex) {
    return knex.schema.dropTableIfExists('ORDERS');
  };
  