// seeds/account_seed.js
exports.seed = function(knex) {
  return knex('ACCOUNT').del()
    .then(function() {
      return knex('ACCOUNT').insert([
        { EMPLOYEE_ID: 1, EMP_NAME: 'Alice Johnson', EMP_ROLE: 'Manager', ACCESS_LEVEL: 3, USERNAME: 'alice.j', USER_PASSWORD: 'password123' },
        { EMPLOYEE_ID: 2, EMP_NAME: 'Bob Brown', EMP_ROLE: 'Barista', ACCESS_LEVEL: 1, USERNAME: 'bob.b', USER_PASSWORD: 'securePass' }
      ]);
    });
};
