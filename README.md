Visual Image Search and Annotation (VISA)
=========================================

VISA is a .Net library and application that allows searching a database of images based on their contents and auto-annotate them. 

 - Provide a way of searching an image library based on the content of a query image provided by the user.
 - Auto-annotate all the images in a library.

Content Based Image Search in VISA
----------------------------------

VISA index images based on two features: region based color histogram and SURF feature.

 - Region Based Color Histogram: We split the image into 4 region and a center region for total 5. For each region we compute a 64 bins histogram for each of the bands and concatenate all of them to form a single vector. The resulted vector is the global feature descriptor.
 - SURF: We use standard OpenCV SURF as texture feature descriptors.

You can select to use both of the above feature for indexing or one of them only. The region based color histogram scale to millions of images, and the SURF can scale to thousand of images.
 
Auto-Annotation
---------------
 
For auto-annotation, we used 3 different algorithms depend on the category of the annotation. Most of the training were done in MatLab, and the resulted model were used in OpenCV. TBD: Add details and references.
 
Dependencies
------------
VISA depends on Emgu Library for all its OpenCV need, you can get the latest Emgu library from: http://www.emgu.com/
  
