// seeds/recipe_seed.js
exports.seed = function(knex) {
  return knex('RECIPE').del()
    .then(function() {
      return knex('RECIPE').insert([
        { BEVERAGE_ID: 1, MATERIAL_ID: 1, QUANTITY: 10 }, // 10 grams of Coffee Beans for Latte
        { BEVERAGE_ID: 1, MATERIAL_ID: 2, QUANTITY: 5 },  // 5 units of Milk for Latte
        { BEVERAGE_ID: 2, MATERIAL_ID: 2, QUANTITY: 5 }   // 5 units of Green Tea for Green Tea
      ]);
    });
};
