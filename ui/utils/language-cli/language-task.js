const program = require('commander');
// Require logic.js file and extract controller functions using JS destructuring assignment
const { getKey, addKey } = require('./logic');

program
    .version('0.0.1')
    .description('Contact management system');


program
    .command('getkey <name>')
    .alias('g')
    .description('Get a key values')
    .option("-l, --language [lang]", "Which language mode to use [en,es...]")
    .action((name, options) => getKey(name, options));

program
    .command('addkey <name>')
    .alias('k')
    .description('add key with language')
    .option("-e, --es [lang]", "spanish value")
    .option("-n, --en [lang]", "inglish value")
    .action((name, options) => addKey(name, options));


program.parse(process.argv);