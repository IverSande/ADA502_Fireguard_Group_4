# Python/grpc/service_impl.py
from concurrent import futures
import grpc
from . import TestService_pb2, TestService_pb2_grpc

class FireRiskGRPCService(TestService_pb2_grpc.TestServiceServicer):
    def SendData(self, request, context):
        # For now, simply return a sample response.
        return TestService_pb2.DataResponse(temperature="22°C")

def serve_grpc():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    TestService_pb2_grpc.add_TestServiceServicer_to_server(FireRiskGRPCService(), server)
    server.add_insecure_port('[::]:50051')
    server.start()
    print("Python gRPC FireRiskGRPCService running on port 50051")
    server.wait_for_termination()
