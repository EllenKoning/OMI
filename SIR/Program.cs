string path = "../../../../../raw_data/";
Filer filer = new Filer(path);
Model model = new Model(filer);
model.run(1000, false); 