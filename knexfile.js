// Update with your config settings.

/**
 * @type { Object.<string, import("knex").Knex.Config> }
 */
require("dotenv").config();

module.exports = {
  development: {
    client: "mysql2",
    connection: {
      host: `${process.env.MYSQL_HOST}`,
      port: parseInt(process.env.MYSQL_PORT),
      user: `${process.env.MYSQL_USERNAME}`,
      password: `${process.env.MYSQL_PASSWORD}`,
      database: `${process.env.MYSQL_DATABASE}`,
    },
  },
};
