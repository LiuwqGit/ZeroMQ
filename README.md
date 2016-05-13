#### [ZeroMQ系列 之NetMQ](http://www.cnblogs.com/liusc/p/5461038.html)####
#### [一：zeromq简介](http://www.cnblogs.com/liusc/p/5461042.html) ####
#### [二：NetMQ 请求响应模式 Request-Reply](http://www.cnblogs.com/liusc/p/5461055.html)  ####
#### [三：NetMQ 发布订阅模式 Publisher-Subscriber](http://www.cnblogs.com/liusc/p/5461060.html) ####
#### [四：NetMQ 推拉模式 Push-Pull](http://www.cnblogs.com/liusc/p/5461184.html) ####

# zeromq简介 #
**`NetMQ` 是  `ZeroMQ`的C#移植版本。**  
**`NetMQ` 版本 3.3.2.2**

## 1：zeromq是什么 ##
`NetMQ` （ZeroMQ to .Net），`ZMQ`号称史上最快中间件。  
它对`socket`通信进行了封装，使得我们不需要写`socket`函数调用就能完成复杂的网络通信。  
它跟`Socket`的区别是：普通的`socket`是端到端的（`1:1`的关系），而`ZMQ`却是可以`N:M`的关系，人们对`BSD`套接字的了解较多的是点对点的连接，点对点连接需要显式地建立连接、销毁连接、选择协议（TCP/UDP）和处理错误等，而`ZMQ`屏蔽了这些细节，让你的网络编程更为简单。  
它是一个消息处理队列库，可在多个线程、内核和主机盒之间弹性伸缩。和一般意义上的消息队列产品不同的是，它没有消息队列服务器，而更像是一个网络通信库。从网络通信的角度看，它处于会话层之上，应用层之下，属于传输层。
## 2：zeromq的消息模型 ##
`zeromq`将消息通信分为4种模型，分别是一对一结对模型（`Exclusive-Pair`）、请求回应模型（`Request-Reply`）、发布订阅模型（`Publish-Subscribe`）、推拉模型（`Push-Pull`）。这4种模型总结出了通用的网络通信模型，在实际中可以根据应用需要，组合其中的2种或多种模型来形成自己的解决方案。  

### 2.1 一对一结对模型 Exclusive-Pair ###
最简单的`1:1`消息通信模型，用来支持传统的 `TCP socket `模型,主要用于进程内部线程间通信。可以认为是一个`TCP Connection`，但是`TCP Server`只能接受一个连接。采用了lock free实现，速度很快。数据可以双向流动，这点不同于后面的请求响应模型。(不推荐使用，没有例子）
### 2.2 请求回应模型 Request-Reply ###
由请求端发起请求，然后等待回应端应答。一个请求必须对应一个回应，从请求端的角度来看是发-收配对，从回应端的角度是收-发对。跟一对一结对模型的区别在于请求端可以是`1~N`个。  
请求端和回应端都可以是`1：N`的模型。通常把`1`认为是`server`,`N`认为是`Client`。`ZeroMQ`可以很好的支持路由功能（实现路由功能的组件叫作`Device`),把`1：N`扩展为`N:M`(只需要加入若干路由节点）。从这个模型看，更底层的端点地址是对上层隐藏的。每个请求都隐含有回应地址，而应用则不关心它。通常把该模型主要用于远程调用及任务分配等。 
[(NetMQ请求响应C#调用案例)](http://www.cnblogs.com/liusc/p/5461055.html "NetMQ 请求响应模式 Request-Reply" )  

 
### 2.3 发布订阅模型 Publisher-Subscriber ###
发布端单向分发数据，且不关心是否把全部信息发送给订阅端。如果发布端开始发布信息时，订阅端尚未连接上来，则这些信息会被直接丢弃。订阅端未连接导致信息丢失的问题，可以通过与请求回应模型组合来解决。订阅端只负责接收，而不能反馈，且在订阅端消费速度慢于发布端的情况下，会在订阅端堆积数据。该模型主要用于数据分发。天气预报、微博明星粉丝可以应用这种经典模型。 [(NetMQ发布订阅模式C#调用案例)](http://www.cnblogs.com/liusc/p/5461060.html "NetMQ 发布订阅模式 Publisher-Subscriber ")
### 2.4 推拉模型 Push-Pull ###
Server端作为Push端，而Client端作为Pull端，如果有多个Client端同时连接到Server端，则Server端会在内部做一个负载均衡，采用平均分配的算法，将所有消息均衡发布到Client端上。与发布订阅模型相比，推拉模型在没有消费者的情况下，发布的消息不会被消耗掉；在消费者能力不够的情况下，能够提供多消费者并行消费解决方案。该模型主要用于多任务并行。  
[(NetMQ推拉模式C#调用案例)](http://www.cnblogs.com/liusc/p/5461184.html "NetMQ 推拉模式 Push-Pull")

## 3：zeromq的优势 ##
- TCP：ZeroMQ基于消息，消息模式，而非字节流。
- XMPP：ZeroMQ更简单、快速、更底层。Jabber可建在ZeroMQ之上。
- AMQP：完成相同的工作，ZeroMQ要快100倍，而且不需要代理（规范更简洁——少278页）
- IPC：ZeroMQ可以跨多个主机盒，而非单台机器。
- CORBA：ZeroMQ不会将复杂到恐怖的消息格式强加于你。
- RPC：ZeroMQ完全是异步的，你可以随时增加/删除参与者。
- RFC 1149：ZeroMQ比它快多了！
- 29west LBM：ZeroMQ是自由软件！
- IBM低延迟：ZeroMQ是自由软件！
- Tibco：仍然是自由软件！