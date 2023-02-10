use std::{
    fs,
    io::{prelude::*, BufReader},
    net::{TcpListener, TcpStream},
};

const STATUS_OK: &str = "HTTP/1.1 200 OK";
const STATUS_NOT_FOUND: &str = "HTTP/1.1 404 OK";
const APP_JSON: &str = "nContent-Type: application/json";

fn main() {
    let listener = TcpListener::bind("127.0.0.1:7878").unwrap();

    for stream in listener.incoming() {
        let stream = stream.unwrap();

        handle_connection(stream);
    }
}

// API routes handler
fn handle_connection(mut stream: TcpStream) {
    let buf_reader = BufReader::new(&mut stream);
    let request_line = buf_reader.lines().next().unwrap().unwrap();

    let response: String;
    let split_req_path: Vec<&str> = request_line.split_whitespace().collect();
    let req_path: &str = split_req_path[1];

    match req_path {
        "/" => { response = index_controller() },
        "/api/repositories/vinifsouza" => { response = api_repositories_controller() },
        _ => response = json_response(STATUS_NOT_FOUND, "".to_string())
    }

    stream.write_all(response.as_bytes()).unwrap();
}

// Controllers
fn index_controller() -> String {
    let contents = fs::read_to_string("src/__mocks__/index.json").unwrap();
    let response = json_response(STATUS_OK, contents);
    return response;
}

fn api_repositories_controller() -> String {
    let contents = fs::read_to_string("src/__mocks__/api/repositories/user.json").unwrap();
    let response = json_response(STATUS_OK, contents);
    return response;
}

// Utils
fn json_response(status: &str, contents: String) -> String {
    return format!("{status}\r\n{APP_JSON}\r\n\r\n{contents}");
}
