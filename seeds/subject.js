/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('subject').del()
  await knex('subject').insert([
    {name: 'Math'},
    {name: 'Literature'},
    {name: 'Physics'},
    {name: 'Geography'},
    {name: 'History'},
    {name: 'Biology'},
    {name: 'Chemistry'},
    {name: 'English'},
  ]);
};
