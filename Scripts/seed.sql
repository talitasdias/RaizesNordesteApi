-- =========================================
-- SEED - RAIZES NORDESTE
-- =========================================
-- Senha padrão para todos usuários:
-- 123456
-- =========================================

-- =========================================
-- USUÁRIOS
-- Roles:
-- Cliente = 1
-- Atendente = 2
-- Cozinha = 3
-- Gerente = 4
-- Admin = 5
-- =========================================

INSERT INTO "Usuarios"
(
    "Nome",
    "Email",
    "SenhaHash",
    "Role",
    "Ativo",
    "PontosFidelidade",
    "AceitouProgramaFidelidade",
    "DataCriacao"
)
VALUES
(
    'Administrador',
    'admin@email.com',
    '$2a$11$N9aTjWbqwbhRlrn4obHI3u4IgarCuS8zf9rwJrJ7nZlDjH79yMR5i',
    5,
    true,
    0,
    true,
    NOW()
),
(
    'Gerente Unidade',
    'gerente@email.com',
    '$2a$11$N9aTjWbqwbhRlrn4obHI3u4IgarCuS8zf9rwJrJ7nZlDjH79yMR5i',
    4,
    true,
    0,
    true,
    NOW()
),
(
    'Funcionário Cozinha',
    'cozinha@email.com',
    '$2a$11$N9aTjWbqwbhRlrn4obHI3u4IgarCuS8zf9rwJrJ7nZlDjH79yMR5i',
    3,
    true,
    0,
    false,
    NOW()
),
(
    'Atendente Loja',
    'atendente@email.com',
    '$2a$11$N9aTjWbqwbhRlrn4obHI3u4IgarCuS8zf9rwJrJ7nZlDjH79yMR5i',
    2,
    true,
    0,
    false,
    NOW()
),
(
    'Cliente Teste',
    'cliente@email.com',
    '$2a$11$N9aTjWbqwbhRlrn4obHI3u4IgarCuS8zf9rwJrJ7nZlDjH79yMR5i',
    1,
    true,
    100,
    true,
    NOW()
);

-- =========================================
-- UNIDADES
-- =========================================

INSERT INTO "Unidades"
(
    "Nome",
    "Endereco",
    "Ativa"
)
VALUES
(
    'Raízes Centro',
    'Av. Eduardo Ribeiro, 100',
    true
),
(
    'Raízes Shopping',
    'Av. Djalma Batista, 500',
    true
),
(
    'Raízes Iranduba',
    'Rua Principal, 200',
    true
);

-- =========================================
-- PRODUTOS
-- =========================================

INSERT INTO "Produtos"
(
    "Nome",
    "Descricao",
    "Preco",
    "Disponivel",
    "DataCriacao"
)
VALUES
(
    'Tacacá',
    'Tacacá tradicional amazônico',
    25.90,
    true,
    NOW()
),
(
    'X-Caboquinho',
    'Sanduíche regional amazônico',
    18.50,
    true,
    NOW()
),
(
    'Açaí 500ml',
    'Açaí com granola',
    15.00,
    true,
    NOW()
),
(
    'Tapioca de Frango',
    'Tapioca recheada com frango',
    14.90,
    true,
    NOW()
),
(
    'Suco de Cupuaçu',
    'Suco natural 300ml',
    8.50,
    true,
    NOW()
),
(
    'Bolo de Macaxeira',
    'Fatia individual',
    10.00,
    true,
    NOW()
);

-- =========================================
-- ESTOQUES - UNIDADE 1
-- =========================================

INSERT INTO "Estoques"
(
    "ProdutoId",
    "UnidadeId",
    "Quantidade",
    "DataCriacao"
)
VALUES
(1, 1, 20, NOW()),
(2, 1, 15, NOW()),
(3, 1, 30, NOW()),
(4, 1, 10, NOW()),
(5, 1, 25, NOW()),
(6, 1, 12, NOW());

-- =========================================
-- ESTOQUES - UNIDADE 2
-- =========================================

INSERT INTO "Estoques"
(
    "ProdutoId",
    "UnidadeId",
    "Quantidade",
    "DataCriacao"
)
VALUES
(1, 2, 8, NOW()),
(2, 2, 5, NOW()),
(3, 2, 20, NOW()),
(4, 2, 0, NOW()),
(5, 2, 18, NOW()),
(6, 2, 6, NOW());

-- =========================================
-- ESTOQUES - UNIDADE 3
-- =========================================

INSERT INTO "Estoques"
(
    "ProdutoId",
    "UnidadeId",
    "Quantidade",
    "DataCriacao"
)
VALUES
(1, 3, 3, NOW()),
(2, 3, 2, NOW()),
(3, 3, 10, NOW()),
(4, 3, 5, NOW()),
(5, 3, 0, NOW()),
(6, 3, 4, NOW());