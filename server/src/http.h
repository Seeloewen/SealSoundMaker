//
// Created by cdlemmi on 06.09.26.
//

#ifndef SEALSOUNDMAKER_HTTPREQUEST_H
#define SEALSOUNDMAKER_HTTPREQUEST_H

#include <string>
#include <map>

enum HTTPContentType {BINARY = 0, HTML, CSS, JS, JSON};



enum HTTPMethod {UNKNOWN = 0, GET, POST};


class HTTPRequest {
public:
    HTTPMethod method;
    std::string uri;
    HTTPContentType contentType;
    std::map<std::string, std::string> cookies;


    HTTPRequest(const std::string packet);

    size_t contentLength;
    char* content = nullptr;




};

#endif //SEALSOUNDMAKER_HTTPREQUEST_H
