шаги по добавлению технологии

- объект TechTree, в редакторе Add 
- настроить параметры технологии
- добавить эту технологию как потомка нужным технологиям, открыв их и нажав Add child
- объект Canvas-TechWindow, скрипт TechnologyTree_Drawer нажать Instantiate/Update techs on canvas, новая технология отрисуется в центре
- установить ее позицию на канвасе вручную
- в TechnologyTree_Drawer нажать CreateConnections, создадутся связи от всех родителей к этой технологии
- при желании можно поправить кривую найдя в Canvas-TechWindow-Container соответствующую связь, в UILineConnector установить Manual Bezier Points и перемещать объекты B1 и B2 до получения удовлетворительного результата