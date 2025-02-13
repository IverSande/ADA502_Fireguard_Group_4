import threading
import logging
from contextlib import asynccontextmanager

from fastapi import FastAPI
from grpcImplementations import TestServiceServer

# Configure logging
logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(name)s - %(levelname)s - %(message)s', handlers=[logging.StreamHandler()])
logger = logging.getLogger(__name__)


@asynccontextmanager
async def lifespan(app: FastAPI):
    logger.info("Starting gRPC server")
    grpc_thread = threading.Thread(target=TestServiceServer.serve(), daemon=True)
    grpc_thread.start()
    yield

app = FastAPI(lifespan)



@app.get("/")
def read_root():
    logger.info("log message")
    return {"message" : "Hello World"}