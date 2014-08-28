<?php

    $secret = "~~yourApiSecret~~";
    $token = "~~yourApiToken~~";
    $ms = time() * 1000;

    $apiUrl = "http://demo.devresults.com/api/awards?t={$token}&ms={$ms}";
    parse_str(parse_url($apiUrl, PHP_URL_QUERY), $queryValues);

    $sortedKeys = array_keys($queryValues);
    sort($sortedKeys);

    $map = array_map(function($key) use (&$queryValues) {
        return $key . "|" . $queryValues[$key] . "|";
    }, $sortedKeys);

    $signature = array_reduce($map, function($x, $y) { return $x . $y; });
    $hashedSignature = hash_hmac("sha256", $signature, $secret, false);

    $apiUrl .= "&s={$hashedSignature}";

    echo "Going to request: {$apiUrl}\n";

    $req = new http\Client\Request("GET", $apiUrl);
    $client = new http\Client;
    $client->enqueue($req)->send();
    $response = $client->getResponse();
    $responseCode = $response->getResponseCode();

    if($responseCode == 200) {
        echo "Success!\n";
    } elseif($responseCode == 401) {
        echo "Unauthorized!\n";
    } else {
        echo "Error!\n";
    }
?>
