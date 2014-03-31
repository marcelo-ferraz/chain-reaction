#Chain Reaction Container

Chain Reaction is a framework focused to achieve multi-dispatch delegate and inversion of control with ease.    
A container conceived to make plugin-injection easy, conditional and dynamic.


####Initialization
```c#
public void Initialize() {

	var _container = Configure
    	.This<SimpleChainReactionContainer>()
        .With(InputFrom.AppConfig())
        .With(InputFrom.Annotations(this.GetType().Assembly))
        .Container;

}
```
####Usage
A plain simple usage, that will return the object and all its listeners
```c#
public void Main(string[] args)
{
    var processor = 
       _container.Invoke<UselessProcessing>();
}

```

If you need access to the listeners, you can catch them all inside the ``` afterLoad``` argument, as follows:
```c#
public void Main(string[] args)
{
	UselessListener listener = null;
    
    var processor = 
       _container.Invoke<UselessProcessing>(
           afterLoad: li => listener = (UselessListener)li);
}

```
Of course, the more _actions_ you set to be listened, the more will appear in the method, which will make the code above throw an ```exception``` for invalid cast.    
You can set all listeners of a given group to attach to the class you provided.
```c#
public void Main(string[] args)
{
	UselessListener listener = null;
    
    var processor = 
       _container.Invoke<UselessProcessing>(
       		group: "oneGroup",
           	afterLoad: li => listener = (UselessListener)li);
}

```
###Configuration
Configuration can be either made by appConfig, by notation, or both. You can Combine the two, so one part can change the behavior of the other.

####Using App.Config
With the xml on app.config, you can have a very visual experience, keeping in mind that the structure of the xml provides such ease.
```xml
  <chainReaction>
    <groups>
      <add name="">
        <sources>
          <add type="ChainReaction.Tests.Model.AppConfig.UselessProcessing, ChainReaction.Tests"/>
          <add type="ChainReaction.Tests.Model.AppConfig.MoreUselessProcessing, ChainReaction.Tests">
            <triggers>
              <add name="ListenedTwice" />
            </triggers>
          </add>
        </sources>
        <handlers>
          <add type="ChainReaction.Tests.Model.AppConfig.Logger, ChainReaction.Tests"></add>
        </handlers>
      </add>        
    </groups>
  </chainReaction>
  ```
####Using Notations (Attributes)
Another mean to create the relations, beetween the events and their listeners, is to use notations.
#####Source Notation
Using the ```SourceAttribute``` You can expose class that holds events, and its events:
```c#
    [Source]
    public class UselessProcessing
    {    	
        [Trigger]
        public event Action<String> Init;
        [Trigger]
        public event Action<String> Middle;
    }
```
> Please, bear in mind that if you do not provide any attribute to any event, the framework will expose all public events to be bound to.

#####Handler Notation
The handler notation, marks the the class that will hold all the methods that will be set to listen to an event. Their main rules are:    

* More than one ```handler``` class can be used to pay atention to a ```source```,
* Only one ```source``` can be specified by the ``handler``,
* More than one ```ActionAttribute``` can be set to a same method, meaning that the same method can listen to any specified event, as long it respects the action rules
* If no ```ActionAttribute``` is provided, the framework will bind using the name __and__ signature. 

######Action Notation
A action marks a method as listener of a given event.

* Any action can listen to any event, as long as the event and the function have the same signature,
* The biding can happen either by name, as long as the names are the same, or if the ``event name`` is provided, it will be bound by that.


```c#
    [Handler]
    public class Logger
    {
        [Action]
        public void ListenToMoreThanOne(string sentence)
        {
            (...)
        }

        [Action]
        [Action(EventName="Init")]
        public void Middle(string sentence) 
        {
            (...)                
        }
    }
```


