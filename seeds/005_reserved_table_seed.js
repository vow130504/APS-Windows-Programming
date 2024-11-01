// seeds/reserved_table_seed.js
exports.seed = function(knex) {
  return knex('RESERVED_TABLE').del()
    .then(function() {
      return knex('RESERVED_TABLE').insert([
        { RESERVED_TABLE_ID: 1, TABLE_ID: 101, IS_RESERVED: true, RESERVATION_TIME: '2024-10-31 12:30:00', SEATS: 4 },
        { RESERVED_TABLE_ID: 2, TABLE_ID: 102, IS_RESERVED: false, RESERVATION_TIME: null, SEATS: 2 }
      ]);
    });
};
