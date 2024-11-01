// seeds/beverage_seed.js
exports.seed = function(knex) {
  return knex('BEVERAGE').del()
    .then(function() {
      return knex('BEVERAGE').insert([
        { ID: 1, BEVERAGE_NAME: 'Latte', CATEGORY_ID: 1, SIZE: 'M', PRICE: 45, RECIPE_ID: 1, IMAGE_PATH: "Assets/soup_paris.jpg" },
        { ID: 2, BEVERAGE_NAME: 'Green Tea', CATEGORY_ID: 2, SIZE: 'L', PRICE: 35, RECIPE_ID: 2, IMAGE_PATH: "Assets/soup_paris.jpg" },
        { ID: 3, BEVERAGE_NAME: 'Tra sua', CATEGORY_ID: 3, SIZE: 'L', PRICE: 35, RECIPE_ID: 2, IMAGE_PATH: "Assets/soup_paris.jpg" },
        { ID: 4, BEVERAGE_NAME: 'Olong', CATEGORY_ID: 1, SIZE: 'L', PRICE: 35, RECIPE_ID: 2, IMAGE_PATH: "Assets/soup_paris.jpg" }
      ]);
    });
};
