// seeds/order_details_seed.js
exports.seed = function(knex) {
  return knex('ORDER_DETAILS').del()
    .then(function() {
      return knex('ORDER_DETAILS').insert([
        { ORDER_DETAILS_ID: 1, ORDER_ID: 1, BEVERAGE_ID: 1, QUANTITY: 1, PRICE: 45, SUBTOTAL: 45 },
        { ORDER_DETAILS_ID: 2, ORDER_ID: 1, BEVERAGE_ID: 2, QUANTITY: 1, PRICE: 35, SUBTOTAL: 35 },
        { ORDER_DETAILS_ID: 3, ORDER_ID: 2, BEVERAGE_ID: 2, QUANTITY: 1, PRICE: 35, SUBTOTAL: 35 }
      ]);
    });
};
