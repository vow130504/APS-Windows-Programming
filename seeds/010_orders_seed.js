// seeds/orders_seed.js
exports.seed = function(knex) {
  return knex('ORDERS').del()
    .then(function() {
      return knex('ORDERS').insert([
        { ORDER_ID: 1, CUSTOMER_ID: 1, RESERVED_TABLE_ID: 1, ORDER_TIME: '2024-10-31 12:40:00', COMPLETED_TIME: '2024-10-31 13:00:00', TOTAL_AMOUNT: 80, PAYMENT_METHOD_ID: 1, EMPLOYEE_ID: 1, PAYMENT_METHOD: 'Credit Card' },
        { ORDER_ID: 2, CUSTOMER_ID: 2, RESERVED_TABLE_ID: 2, ORDER_TIME: '2024-10-31 13:10:00', COMPLETED_TIME: '2024-10-31 13:30:00', TOTAL_AMOUNT: 35, PAYMENT_METHOD_ID: 2, EMPLOYEE_ID: 2, PAYMENT_METHOD: 'Cash' }
      ]);
    });
};
