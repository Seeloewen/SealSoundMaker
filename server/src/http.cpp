//
// Created by cdlemmi on 06.09.26.
//

#include "http.h"

#include <stdexcept>




HTTPContentType parseContentType(std::string value) {
    std::string contentType = value.substr(0, value.find(";"));
    if (contentType == "application/json") {
        return JSON;
    } else if (contentType == "application/js") {
        return JS;
    } else if (contentType == "text/html") {
        return HTML;
    } else if (contentType == "text/css") {
        return CSS;
    } else {
        return BINARY;
    }

}


HTTPRequest::HTTPRequest(const std::string packet) : method(UNKNOWN) {
    size_t current_pos = 0;
    size_t line_end = packet.find("\r\n");
    std::string line = packet.substr(0, line_end);
    size_t method_end = line.find(" ");
    std::string method_s = line.substr(0, method_end);
    if (method_s == "GET") {
        method = GET;
    } else if (method_s == "POST") {
        method = POST;
    }
    size_t uri_end = line.find(" ", method_end +1);
    size_t uri_start = method_end + 1;
    uri = line.substr(uri_start, uri_end - uri_start);

    std::string http_version = line.substr(uri_end + 1, std::string::npos);
    if (http_version != "HTTP/1.1") throw "http parser error: incompatible protocol version";

    while (true) {
        current_pos = line_end + 2;
        line_end = packet.find("\r\n", current_pos);
        if (line_end == current_pos) break;
        line = packet.substr(current_pos, line_end - current_pos);
        size_t colon_pos = line.find(":");
        std::string name = line.substr(0, colon_pos);
        std::string field = line.substr(colon_pos + 2);
        if (name == "Host") {
            if (field != "www.waybersite.de") {
                throw "http parser error: incorrect host name";
            }
        } else if (name == "Content-Length") {
            contentLength = std::stol(field);
        } else if (name == "Content-Type") {
            contentType = parseContentType(field);
        } else if (name == "Cookie") {
            size_t cookie_pos = 0;
            size_t space_pos = 0;
            while (space_pos != std::string::npos) {
                space_pos = field.find(' ', cookie_pos);
                std::string cookie = field.substr(cookie_pos, space_pos - cookie_pos);
                cookie_pos = space_pos + 1;
                size_t eq_pos = cookie.find("=");
                std::string key = cookie.substr(0, eq_pos);
                std::string value = cookie.substr(eq_pos + 1, std::string::npos);
                cookies[key] = value;
            }
        }

   }






}








