# Banco de Dados e Serviços (BSD)

O Banco de Dados e Serviços (BSD) é uma solução desenvolvida para o gerenciamento eficiente de horas extras, oferecendo uma maneira simplificada e transparente de rastrear e alocar horas extras com base em categorias de serviços específicas. Desenvolvido como parte das atividades de estágio no Porto de Recife SA, o BSD tem como objetivo principal otimizar o processo de registro e gerenciamento de horas extras, proporcionando benefícios significativos em termos de economia de tempo e precisão nos registros.

### Principais Características e Benefícios:

- Facilita o rastreamento e a alocação de horas extras de forma eficiente e transparente.
- Contribui para a precisão e transparência no registro de horas extras, aumentando a confiabilidade dos dados.
- Oferece flexibilidade e adaptabilidade para atender às necessidades específicas de gerenciamento de horas extras da empresa.

### Tecnologias Utilizadas e Arquitetura do Projeto:

O BSD está sendo desenvolvido utilizando a linguagem de programação C#, seguindo uma abordagem de arquitetura limpa e adotando o padrão MVC (Model-View-Controller) para estruturar o projeto. A regra de negócio é completamente desacoplada dos componentes de interface e persistência de dados, garantindo flexibilidade e manutenibilidade.

---
### Definição de Requisitos do Sistema

#### Entradas:

As entradas para o sistema BSD consistem principalmente em duas informações essenciais: a matrícula do funcionário com seu dígito verificador e a data do serviço realizado.

1. **Matrícula e Dígito Verificador:**

   - A matrícula de um funcionário, juntamente com seu dígito verificador, é a identificação única associada a cada indivíduo. O sistema deve garantir que a matrícula seja válida, seguindo critérios específicos:
     - A matrícula deve conter exatamente 6 dígitos numéricos.
     - O primeiro dígito não pode ser zero.
   - Além disso, o dígito verificador precisa ser verificado utilizando o algoritmo do módulo 11, garantindo que esteja correto.

2. **Data do Serviço:**
   - A data do serviço é crucial para determinar os atributos correspondentes, como o tipo de dia (útil, domingo, feriado ou domingo/ feriado). O sistema deve validar a data inserida seguindo o formato 'dd/MM/aaaa' e garantir sua validade.

#### Processamento:

Após receber as entradas do usuário, o sistema executa uma série de processos para verificar e processar as informações fornecidas:

1. **Verificação de Matrícula e Dígito Verificador:**

   - O sistema realiza a validação da matrícula e do dígito verificador para garantir sua integridade.
   - Primeiramente, verifica-se se a matrícula está no formato correto e se o dígito verificador está de acordo com o algoritmo do módulo 11.
   - Em seguida, consulta-se a base de dados para confirmar se a matrícula corresponde a um funcionário cadastrado. Caso contrário, uma mensagem adequada é retornada.

2. **Verificação da Data do Serviço:**

   - O sistema valida a data do serviço inserida pelo usuário para garantir que esteja no formato correto e seja uma data válida.
   - Essa validação é essencial para determinar o tipo de dia associados a cada entrada.

3. **Atribuição de Rubricas:**
   - Com base na data do serviço e no tipo de serviço do funcionário, o sistema atribui rubricas específicas.
   - Atribui-se um tipo de dia (útil, domingo, feriado) e um tipo de serviço (P140 ou P110) para cada funcionário, o que influencia diretamente nas rubricas atribuídas.

#### Saídas:

O sistema BSD retorna diversas saídas, incluindo mensagens de erro e relatórios detalhados sobre as informações processadas:

1. **Mensagens de Erro:**

   - Se alguma entrada estiver incorreta ou não atender aos critérios estabelecidos, o sistema emite mensagens de erro apropriadas para orientar o usuário.
   - Essas mensagens garantem uma interação clara e informativa com o usuário, ajudando a corrigir quaisquer problemas identificados.

2. **Relatório:**
   - O relatório gerado pelo sistema BSD contém informações cruciais sobre os funcionários e os serviços realizados dentro de um intervalo de datas especificado.
   - Cada relatório inclui detalhes como matrícula e dígito verificador dos funcionários, códigos de rubrica de horas extras e o total de horas trabalhadas por cada funcionário.

---

### Cadastro de Funcionários no Sistema

#### Processo de Cadastro:

O processo de cadastro de funcionários permite que novos membros sejam adicionados ao sistema BSD. Para isso, o usuário deverá inserir duas informações principais: a matrícula e o tipo de serviço que o funcionário executa.

1. **Entradas:**

   - Matrícula: Identificação única de 6 dígitos numéricos. O usuário deve inserir a matrícula completa, sem o dígito verificador.
   - Tipo de Serviço: O usuário seleciona o tipo de serviço que o funcionário executa, que pode ser P140 ou P110.

2. **Validações:**

   - **Matrícula:**
     - Verificar se a matrícula está no formato correto, consistindo de 6 dígitos numéricos.
     - O primeiro dígito da matrícula não pode ser zero.
   - **Tipo de Serviço:**
     - Verificar se o tipo de serviço inserido pelo usuário é válido, ou seja, se é P140 ou P110.

3. **Processamento:**

   - Após receber as entradas do usuário, o sistema realizará as seguintes ações:
     - Validar a matrícula e o tipo de serviço conforme descrito acima.
     - Consultar a base de dados para garantir que a matrícula não esteja duplicada e que o tipo de serviço seja válido.

4. **Saídas:**
   - Se todas as validações forem bem-sucedidas, o sistema cadastra o funcionário e retorna uma mensagem de confirmação.
   - Caso contrário, o sistema retorna uma mensagem de erro específica indicando o problema encontrado.

---

### Criação de Rubricas no Sistema

#### Processo de Criação:

O processo de criação de rubricas permite que novas rubricas sejam adicionadas ao sistema BSD. Para isso, o usuário deverá inserir três informações principais: o código da rubrica, a quantidade de horas por dia e o tipo de serviço associado.

1. **Entradas:**

   - Código da Rubrica: Identificação única de 4 dígitos numéricos, onde o primeiro dígito não pode ser zero.
   - Quantidade de Horas por Dia: Valor fixo determinado pela tabela interna da empresa.
   - Tipo de Serviço: O usuário seleciona o tipo de serviço associado à rubrica, que pode ser P140 ou P110.

2. **Validações:**

   - **Código da Rubrica:**
     - Verificar se o código da rubrica está no formato correto, consistindo de 4 dígitos numéricos.
     - O primeiro dígito do código não pode ser zero.
   - **Quantidade de Horas por Dia:**
     - Garantir que o valor inserido para a quantidade de horas por dia seja válido e esteja de acordo com a política da empresa.
   - **Tipo de Serviço:**
     - Verificar se o tipo de serviço inserido pelo usuário é válido, ou seja, se é P140 ou P110.

3. **Processamento:**

   - Após receber as entradas do usuário, o sistema realizará as seguintes ações:
     - Validar o código da rubrica, a quantidade de horas por dia e o tipo de serviço conforme descrito acima.
     - Consultar a base de dados para garantir que a rubicra não esteja duplicada.

4. **Saídas:**
   - Se todas as validações forem bem-sucedidas, o sistema cria a rubrica e retorna uma mensagem de confirmação.
   - Caso contrário, o sistema retorna uma mensagem de erro específica indicando o problema encontrado.

---

### Funcionamento do Sistema BSD

#### Processo de Cálculo de Horas Extras:

O sistema BSD opera de acordo com regras específicas estabelecidas pela empresa, visando calcular o total de horas extras trabalhadas por cada funcionário em um determinado período. Esse cálculo é baseado nas rubricas associadas a cada funcionário, as quais são atribuídas com base no tipo de dia (útil, domingo, feriado) determinado pela data do serviço no momento do cadastro do BSD.

1. **Determinação do Tipo de Dia:**

   - Com base na data do serviço especificada ao criar o BSD, o sistema determina o tipo de dia (útil, domingo, feriado).

2. **Atribuição de Rubricas:**

   - O sistema associa as rubricas apropriadas a cada funcionário com base no tipo de dia determinado:
     - Se o funcionário trabalhou em um feriado, a rubrica correspondente a esse serviço é associada a ele.
     - Várias rubricas podem ser associadas ao mesmo funcionário no mesmo dia, dependendo das circunstâncias do serviço.

3. **Verificação de Tipos de Serviço:**

   - Antes de atribuir uma rubrica a um funcionário, o sistema verifica se o tipo de serviço da rubrica é compatível com o tipo de serviço do funcionário. A rubrica só será atribuída se os tipos de serviço forem iguais.

4. **Cálculo de Horas Extras:**
   - Para cada funcionário dentro do período especificado, o sistema calcula as horas extras multiplicando a quantidade de rubricas atribuídas a ele pelo número de dias em que elas aparecem e pelo total de horas por dia de cada rubrica.

#### Armazenamento de Informações:

Todas as informações relativas aos BSDs, funcionários, rubricas e horas extras calculadas são armazenadas em um banco de dados para consultas posteriores.

---

### Geração do Relatório CSV

#### Processo de Geração:

O processo de geração do relatório CSV consiste em transformar as informações registradas no BSD em linhas formatadas de acordo com o padrão exigido pelo sistema do governo federal.

1. **Registro de Informações no BSD:**

   - O sistema BSD registra as informações relativas aos funcionários, rubricas e horas trabalhadas dentro de um período especificado.

2. **Transformação em Linhas do Relatório CSV:**

   - Para cada funcionário, o sistema BSD gera uma linha no relatório CSV contendo as seguintes informações:
     - Código do funcionário (prefixo '0000').
     - Matrícula do funcionário.
     - Dígito verificador (prefixo '0' caso contenha apenas um dígito).
     - Código da rubrica.
     - Total de horas trabalhadas no período.

3. **Repetição para Todos os Funcionários:**
   - O processo é repetido para todos os funcionários registrados no BSD dentro do período especificado, gerando uma linha no relatório para cada combinação de funcionário e rubrica.

#### Exemplo do Formato do Relatório CSV:

```
0000+matricula do funcionário um+0+digito;codigo da rubrica um;total de horas trabalhadas no periodo
0000+matricula do funcionário um+0+digito;codigo da rubrica dois;total de horas trabalhadas no periodo
0000+matricula do funcionário um+0+digito;codigo da rubrica três;total de horas trabalhadas no periodo

0000+matricula do funcionário dois+0+digito;codigo da rubrica um;total de horas trabalhadas no periodo
0000+matricula do funcionário dois+0+digito;codigo da rubrica dois;total de horas trabalhadas no periodo
0000+matricula do funcionário dois+0+digito;codigo da rubrica três;total de horas trabalhadas no periodo

0000+matricula do funcionário três+0+digito;codigo da rubrica um;total de horas trabalhadas no periodo
0000+matricula do funcionário três+0+digito;codigo da rubrica dois;total de horas trabalhadas no periodo
0000+matricula do funcionário três+0+digito;codigo da rubrica três;total de horas trabalhadas no periodo
```

#### Armazenamento e Exportação do Relatório:

Após a geração, o relatório CSV é armazenado e pode ser exportado para uso pelo sistema do governo federal.

---

### Estado do Projeto:

Por favor, note que o BSD ainda está em desenvolvimento e pode sofrer alterações à medida que o projeto avança. Manterei atualizações regulares sobre o progresso do projeto.
