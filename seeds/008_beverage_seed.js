// seeds/beverage_seed.js
exports.seed = function(knex) {
  return knex('BEVERAGE').del()
    .then(function() {
      return knex('BEVERAGE').insert([
        { ID: 1, BEVERAGE_NAME: 'Latte', CATEGORY_ID: 1, SIZE: 'M', PRICE: 45, RECIPE_ID: 1 },
        { ID: 2, BEVERAGE_NAME: 'Green Tea', CATEGORY_ID: 2, SIZE: 'L', PRICE: 35, RECIPE_ID: 2 }
      ]);
    });
};
