const fs = require('fs');
const fullpath = '../../src/assets/i18n/';



let getOneLanguage = function (options, name) {
    const language = readFile(fullpath + options.language + '.json');

    if (language.hasOwnProperty(name)) {
        console.log('LANGUAGE=%s KEY=%s VALUE=%s ', options.language, name, language[name]);
    }
    else {
        console.log('Key=%s not found for language=%s', name, options.language);
    }

};

const getKey = (name, options) => {
    options.language = options.language || 'ALL';

    if (options.language === 'ALL') {
        const filesNames = fs.readdirSync(fullpath);
        filesNames.forEach((filename) => {
            getOneLanguage({ language: filename.split('.')[0] }, name)
        })
    }
    else {
        getOneLanguage(options, name);
    }

};

const addKey = (name, options) => {
    const filesNames = fs.readdirSync(fullpath);
    filesNames.forEach((filename) => {
        let jsonData = readFile(fullpath + filename);
        if (filename.indexOf('es') == 0) {
            jsonData[name] = options.es;
        }
        else {
            jsonData[name] = options.en;
        }

        fs.writeFile(fullpath + filename, JSON.stringify(jsonData), function (err) {
            if (err) {
                return console.log(err);
            }
            console.log("The file was saved!");
        });
    });

};

const readFile = (dir) => {
    let rawString = fs.readFileSync(dir, "utf8");
    return JSON.parse(rawString);
};
module.exports = { addKey, getKey };
