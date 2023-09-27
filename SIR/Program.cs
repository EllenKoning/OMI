string path = "../../../../../data/";
Filer filer = new Filer(path);
Model model = new Model(filer, new Demographic(), new Disease());
model.run(250, false);
