# GDD Express: Lucky Mine 3D

## Unity Game Jam --- 4 días

## 1. Visión general y setup técnico

-   **Perspectiva:** Escena 3D con cámara ortográfica (o perspectiva
    fija a 0° pitch/yaw) para lograr el *look & feel* 2D clásico de
    Terraria, pero aprovechando iluminación 3D, físicas de partículas 3D
    y profundidad de capas en el eje Z.
-   **Concepto:** Juego de minería incremental donde la resistencia de
    los bloques, el multiplicador de venta y las rachas de recolección
    dependen de los **Números de la Suerte** (1, 3, 7, 9, 13, 15, 21,
    25, 31, 33, 37, 43, 49...).
-   **Gameplay Loop:** Generar recursos → Comprar mejoras → Incrementar
    producción → Automatizar → Desbloquear sistemas.

## 2. Core Gameplay Loop (adaptado al tema)

El flujo principal representado en el diagrama es:

1.  **Picado en Mina 3D (Vista 2D)** --- Acumular minerales.
2.  **Ascensor a Superficie.**
3.  **Comprar Picos y Auto-Miners** --- Automatizar la extracción.
4.  **Evento / Desafío Temporal** --- Aplicar el multiplicador Lucky
    Number y volver al ciclo de minería.

### Mecánicas del ciclo

1.  **Generación / Extracción:** Romper bloques 3D haciendo clic o
    usando automatizaciones. La cantidad de durabilidad/golpes de cada
    bloque sigue los Lucky Numbers.
2.  **Ciclo de Entrega (Superficie Obligatoria):** Debes subir a la
    superficie (mediante ascensor o subir por la mina) cuando el
    contador total de tu inventario coincida con un Lucky Number para
    multiplicar tus ganancias. Subir con una cantidad que no sea de la
    suerte aplica el precio base sin multiplicador.
3.  **Mejoras:** Invertir ganancias en potencia de pico, velocidad de
    picado y mineros automáticos (robots/torretas 3D).
4.  **Desafíos Temporales (cada 2 minutos):** Se activa una «Racha de
    Suerte» pidiendo picar una cantidad exacta de minerales que coincida
    con un Lucky Number específico a cambio de un bufo masivo de
    velocidad/ganancias.

## 3. Integración del tema Lucky Numbers en 3D

-   **Resistencia de Bloques:** Los bloques tienen puntos de impacto (1,
    3, 7, 13... hits). Golpear un bloque con un pico cuya potencia
    iguale exactamente el valor del bloque produce una explosión de
    gemas de recompensa doble.
-   **Coordenadas de Profundidad (Eje Y):** La probabilidad de encontrar
    minerales raros aumenta al alcanzar profundidades en coordenadas Y
    que sean Lucky Numbers (Y = −7, −13, −21, −31...).
-   **Combos de Venta:** Entregar en la superficie exactamente 1, 3, 7,
    9, 13, 15... minerales aplica un multiplicador exponencial al dinero
    recibido.
-   **Árbol de Mejoras Simples:** Nodos desbloqueables al alcanzar
    profundidades o ventas marcadas por Lucky Numbers.

## 4. Estructura de mina y biomas en 3D

Para evitar la complejidad de un mapa procedural completo en 4 días, se
implementa una **mina 3D basada en cuadrícula (Grid/Tilemap 3D)** con 3
biomas verticales marcados por profundidad:

1.  **Capa 1 --- Superficie / Cobre (Y = 0 a −15):** Bloques de tierra y
    cobre (1 a 3 golpes).
2.  **Capa 2 --- Media / Hierro y Oro (Y = −15 a −45):** Bloques de roca
    dura (7 a 13 golpes).
3.  **Capa 3 --- Rock Bottom / Lucky Ore (Y \< −45):** Minerales
    valiosos con alta resistencia (21, 31, 49 golpes).

## 5. Plan de producción de 4 días (Unity Jam)

| Día | Objetivo principal | Entregables clave |
|:---:|---|---|
| **Día 1** | **Setup 3D/2D y Picado** | Cámara ortográfica 3D, controlador de personaje 2.5D y Raycast 3D para detectar el picado de bloques en cuadrícula. |
| **Día 2** | **Mecánicas Lucky Numbers** | Sistema de durabilidad de bloques basado en Lucky Numbers, multiplicador de inventario al subir a la superficie y evento aleatorio de 2 minutos. |
| **Día 3** | **UI, Tienda y Auto-Miners** | Interfaz UI de compra de picos y mineros automáticos 3D, barra de energía/cansancio que obliga a volver a la superficie. |
| **Día 4** | **Iluminación, Polishing y Build** | Luces puntuales 3D, efectos de partículas al romper bloques, balanceo de economía rápida y exportación de la build en Itch.io. |
