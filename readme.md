
Nombre completo: Nicolás Gomez.


1 - En C#, ¿para qué sirve una propiedad?. ***En C# se utiliza a las propiedades para poder leer, escribir o calcular un campo privado de la clase a través de una manera pública, a la vez que se oculta el código de implementación o verificación.***
	
2 - ¿Cuándo utilizaría acceso protegido en los miembros de una clase?. 

***Utilizaría un acceso protegido en una clase cuando sé que esa clase es padre de una o muchas clases y quiero que solamente la clase padre y sus clases hijas accedan a esos miembros.***


3 - ***En UML, ese tipo de relación es Realización y sirve para indicar que una clase implementa a una interfaz.***


4 - Explique con sus palabras qué implica una relación de Dependencia entre dos clases.	

***Una relación de Dependencia entre clases significa que una clase utiliza a otra. Por ejemplo, si en un método de la clase A necesitara calcular la raíz cuadrada de algo, utilizaría al método Sqrt() de la clase Math y eso provocaría una dependencia.***


5 - Indique V o F según corresponda. Si es F, fundamente su respuesta:
	Un constructor es un método que se invoca de forma automática cuando se instancia el objeto de la clase. ***V***
	Un constructor debe tener siempre el mismo nombre de la clase.  ***V***
	Un constructor puede retornar un valor. ***F -> Un constructor nunca retorna un valor, es un método especial que solamente se llama al realizar una nueva instancia de la clase.***
	Un constructor puede ser privado. ***V***
	Una clase sólo puede tener declarado un único constructor. ***F -> Se puede tener más de un constructor en la clase, por ejemplo: un constructor sin parámetros, otro con parámetros, etc.***
