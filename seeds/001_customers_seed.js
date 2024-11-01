// seeds/customers_seed.js
exports.seed = function(knex) {
  return knex('CUSTOMERS').del()
    .then(function() {
      return knex('CUSTOMERS').insert([
        { CUSTOMER_ID: 1, CUSTOMER_NAME: 'John Doe', PHONE_NUMBER: '1234567890', EMAIL: 'john.doe@example.com', POINTS: 100, REWARDS: 'Free Coffee' },
        { CUSTOMER_ID: 2, CUSTOMER_NAME: 'Jane Smith', PHONE_NUMBER: '0987654321', EMAIL: 'jane.smith@example.com', POINTS: 150, REWARDS: '10% Discount' }
      ]);
    });
};
