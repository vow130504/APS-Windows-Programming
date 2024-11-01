// seeds/revenue_seed.js
exports.seed = function(knex) {
  return knex('REVENUE').del()
    .then(function() {
      return knex('REVENUE').insert([
        { REVENUE_ID: 1, ORDER_ID: 1, REVENUE_DATE: '2024-10-31', TOTAL_AMOUNT: 80 },
        { REVENUE_ID: 2, ORDER_ID: 2, REVENUE_DATE: '2024-10-31', TOTAL_AMOUNT: 35 }
      ]);
    });
};
