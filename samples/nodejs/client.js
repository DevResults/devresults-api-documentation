var http = require('http');
var crypto = require('crypto');
var url = require('url');

var secret = '~~yourApiSecret~~';
var token = '~~yourApiToken~~';
var ms = new Date().getTime();

var apiUrl = 'http://demo.devresults.com/api/awards?t=' + token + '&ms=' + ms;
var parsedApiUrl = url.parse(apiUrl ,true);
var query= parsedApiUrl.query;

var keys = [];
var sigBase = '';

for(var key in query) {
    if(query.hasOwnProperty(key)) {
        keys.push(key);
    }
}

keys.sort();

for(var i = 0; i < keys.length; i++) {
    sigBase += keys[i];
    sigBase += '|';
    sigBase += query[keys[i]];
    sigBase += '|';
}

var sig = crypto.createHmac('sha256', secret).update(sigBase).digest('hex');

console.log('');
console.log('Unhashed Signature: ' + sigBase);
console.log('Signature:          ' + sig);

apiUrl += '&s=' + sig;

console.log('Requesting:         ' + apiUrl);

console.log('');

http.get(apiUrl, function(res) {
    if(res.statusCode === 401) {
        console.log('Unauthorized!');
    } else {
        console.log('Success!');
    }

    process.exit(0);
}).on('error', function(e) {
    console.log('You suck! Got error: ' + e);
    process.exit(0);
});
