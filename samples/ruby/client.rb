require 'net/http'
require 'openssl'
require 'cgi'

secret = "~~yourApiSecret~~"
token = "~~yourApiToken~~"
ms = Time.now.to_i * 1000

apiUrl = "http://demo.devresults.com/api/awards?t=#{token}&ms=#{ms}"
uri = URI.parse(apiUrl)
queryValues = CGI::parse(uri.query)

sortedKeys = queryValues.keys.sort

signature = ""

sortedKeys.each { |k|
    signature << "#{k}|#{queryValues[k][0]}|"
}

hashedSignature = OpenSSL::HMAC.hexdigest("sha256", secret, signature)

apiUrl << "&s=#{hashedSignature}"

puts "Going to request: #{apiUrl}"

req = Net::HTTP::Get.new(apiUrl)
res = Net::HTTP.start(uri.host, uri.port) { |http|
    http.request(req)
}

if res.code == "200"
    puts "Success!"
elsif res.code == "401"
    puts "Unauthorized!"
else
    puts "Error!"
end
