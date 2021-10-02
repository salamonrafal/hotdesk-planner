using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Moq;

namespace Unit.Helpers
{
    internal static class MocksExtensions
    {
        public static Mock<IAsyncCursor<T>> CreateAsyncCursor<T>(IEnumerable<T> outputData)
        {
            var asyncCursorMock = new Mock<IAsyncCursor<T>> ();
            
            asyncCursorMock
                .Setup (_ => _.Current)
                .Returns (outputData);
            
            asyncCursorMock
                .SetupSequence (_ => _.MoveNext (It.IsAny<CancellationToken> ()))
                .Returns (true)
                .Returns (false);
            
            asyncCursorMock
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

            return asyncCursorMock;
        }
    }
}