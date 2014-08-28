import hashlib
import hmac
import calendar
import time
import urlparse
import urllib2

secret = bytes("~~yourApiSecret~~").encode("utf-8")
token = "~~yourApiToken~~"
ms = calendar.timegm(time.gmtime()) * 1000

apiUrl = "http://demo.devresults.com/api/awards?t=%s&ms=%s" % (token, ms)
uri = urlparse.urlparse(apiUrl)
queryValues = urlparse.parse_qs(uri.query)

sortedKeys = queryValues.keys()
sortedKeys.sort()

signature = reduce(lambda x, y: x + y,
    map(lambda key: key + "|" + queryValues[key][0] + "|", sortedKeys))
sigbytes = bytes(signature).encode("utf-8")

hashedSignature = hmac.new(secret, sigbytes, digestmod=hashlib.sha256).hexdigest()
apiUrl += ("&s=%s" % hashedSignature)

print "Going to request: %s" % (apiUrl)

try:
    res = urllib2.urlopen(apiUrl)
    code = res.getcode()

    if code == 200:
        print "Success!"

except urllib2.HTTPError as e:
    if e.code == 401:
        print "Unauthorized!"
    else:
        print "Error!"
