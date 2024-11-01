/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('learning_method').del()
  await knex('learning_method').insert([
    {name: 'Online'},
    {name: 'Offline'},
  ]);
};
