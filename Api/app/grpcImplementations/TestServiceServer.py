from concurrent import futures
import logging

import grpc
from Api.app.generatedCode import TestService_pb2
from Api.app.generatedCode import TestService_pb2_grpc


class TestServiceServer(TestService_pb2_grpc.TestServiceServicer):
    def SayHello(self, request, context):
        return TestService_pb2_grpc.DataResponse(temperature="5 degrees")


def serve():
    port = "50051"
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    TestService_pb2_grpc.add_TestServiceServicer_to_server(TestServiceServer(), server)
    server.add_insecure_port("[::]:" + port)
    server.start()
    print("Server started, listening on " + port)
    server.wait_for_termination()

if __name__ == "__main__":
    logging.basicConfig()
    serve()