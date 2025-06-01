using Moq;
using AutoMapper;
using Yungching.Application.Orders.Handlers;
using Yungching.Domain.Interfaces.Repositories;
using Yungching.Domain.Entities;
using Yungching.Application.Orders.DTOs;
using Yungching.Application.Orders.Commands;
using Yungching.Application.Orders.Queries;

public class OrderHandlersTests
{
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly OrderHandlers _handler;

    public OrderHandlersTests()
    {
        _orderRepoMock = new Mock<IOrderRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new OrderHandlers(_orderRepoMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_GetOrdersQuery_ReturnsMappedOrderDtos()
    {
        // Arrange
        var orders = new List<Order> { new Order { OrderId = 1 }, new Order { OrderId = 2 } };
        var orderDtos = new List<OrderDto> { new OrderDto { OrderId = 1 }, new OrderDto { OrderId = 2 } };

        _orderRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(orders);
        _mapperMock.Setup(m => m.Map<IEnumerable<OrderDto>>(orders)).Returns(orderDtos);

        // Act
        var result = await _handler.Handle(new GetOrdersQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, ((List<OrderDto>)result).Count);
        _orderRepoMock.Verify(r => r.GetAllAsync(), Times.Once);
        _mapperMock.Verify(m => m.Map<IEnumerable<OrderDto>>(orders), Times.Once);
    }

    [Fact]
    public async Task Handle_GetOrderByIdQuery_ReturnsMappedOrderDto()
    {
        // Arrange
        var order = new Order { OrderId = 1 };
        var orderDto = new OrderDto { OrderId = 1 };

        _orderRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(order);
        _mapperMock.Setup(m => m.Map<OrderDto?>(order)).Returns(orderDto);

        // Act
        var result = await _handler.Handle(new GetOrderByIdQuery(1), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.OrderId);
        _orderRepoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        _mapperMock.Verify(m => m.Map<OrderDto?>(order), Times.Once);
    }

    [Fact]
    public async Task Handle_CreateOrderCommand_ReturnsNewOrderId()
    {
        // Arrange
        var createOrderDto = new CreateOrderDto();
        var order = new Order { OrderId = 1 };
        var newOrderId = 123;

        _mapperMock.Setup(m => m.Map<Order>(createOrderDto)).Returns(order);
        _orderRepoMock.Setup(r => r.CreateAsync(order)).ReturnsAsync(newOrderId);

        // Act
        var result = await _handler.Handle(new CreateOrderCommand(createOrderDto), CancellationToken.None);

        // Assert
        Assert.Equal(newOrderId, result);
        _mapperMock.Verify(m => m.Map<Order>(createOrderDto), Times.Once);
        _orderRepoMock.Verify(r => r.CreateAsync(order), Times.Once);
    }

    [Fact]
    public async Task Handle_UpdateOrderCommand_ReturnsTrueOnSuccess()
    {
        // Arrange
        var updateOrderDto = new UpdateOrderDto { OrderId = 1 };
        var order = new Order { OrderId = 1 };

        _mapperMock.Setup(m => m.Map<Order>(updateOrderDto)).Returns(order);
        _orderRepoMock.Setup(r => r.UpdateAsync(order)).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(new UpdateOrderCommand(updateOrderDto), CancellationToken.None);

        // Assert
        Assert.True(result);
        _mapperMock.Verify(m => m.Map<Order>(updateOrderDto), Times.Once);
        _orderRepoMock.Verify(r => r.UpdateAsync(order), Times.Once);
    }

    [Fact]
    public async Task Handle_DeleteOrderCommand_ReturnsTrueOnSuccess()
    {
        // Arrange
        var orderId = 1;

        _orderRepoMock.Setup(r => r.DeleteAsync(orderId)).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(new DeleteOrderCommand(1), CancellationToken.None);

        // Assert
        Assert.True(result);
        _orderRepoMock.Verify(r => r.DeleteAsync(orderId), Times.Once);
    }
}
