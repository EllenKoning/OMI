
string path = "../../../../data/";
Filer filer = new Filer(path);
Demographic demographic = new Demographic();
Disease disease = new Disease();
Model model = new Model(filer, demographic, disease);
model.run(500, false);
