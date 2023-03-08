import socket
import time
import random


if __name__ == "__main__":
    print("Starting socket!")
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.setsockopt(socket.IPPROTO_TCP, socket.TCP_NODELAY, 1)
        s.bind(('192.168.2.51', 12345))
        # s.bind(('127.0.0.1', 12346))
        s.listen()
        conn, addr = s.accept()
        print(f"Connected by {addr}")
        while True:
            # data = conn.recv(1024)
            # if not data:
            #     break
            r = random.randint(10,1000)
            conn.sendall(str.encode(str(r) + '\n'))
            print(r)
            time.sleep(1)
        conn.close()
