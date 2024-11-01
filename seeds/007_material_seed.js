// seeds/material_seed.js
exports.seed = function(knex) {
  return knex('MATERIAL').del()
    .then(function() {
      return knex('MATERIAL').insert([
        { ID: 1, MATERIAL_NAME: 'Coffee Beans', STOCK: 50, EXP_DATE: '2025-01-01', IMPORT_DATE: '2024-10-01' },
        { ID: 2, MATERIAL_NAME: 'Milk', STOCK: 20, EXP_DATE: '2024-11-15', IMPORT_DATE: '2024-10-20' }
      ]);
    });
};
